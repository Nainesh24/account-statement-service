using FluentAssertions;
using Moq;
using StatementService.Models;
using StatementService.Repositories.Interface;
using StatementService.Services;
using Xunit;

namespace StatementService.Tests.UnitTest
{
    public class StatementServiceTests
    {
        private readonly Mock<ITransactionRepository> _repoMock;
        private readonly Mock<ILogger<StatementServices>> _loggerMock;
        private readonly StatementServices _service;

        public StatementServiceTests()
        {
            _repoMock = new Mock<ITransactionRepository>();
            _loggerMock = new Mock<ILogger<StatementServices>>();

            _service = new StatementServices(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetStatement_ShouldReturnTransactions_WhenDataExists()
        {
            // Arrange
            var accountId = 1;
            var from = DateTime.UtcNow.AddDays(-10);
            var to = DateTime.UtcNow;

            var transactions = new List<Transaction>
            {
                new Transaction { Amount = 100, Type = "credit", CreatedAt = DateTime.UtcNow },
                new Transaction { Amount = 50, Type = "debit", CreatedAt = DateTime.UtcNow }
            };

            _repoMock.Setup(r => r.GetTransactions(accountId, from, to, 0, 10))
                .ReturnsAsync(transactions);

            _repoMock.Setup(r => r.GetSummary(accountId, from, to))
                .ReturnsAsync((100, 50));

            // Act
            var result = await _service.GetStatement(accountId, from, to, 1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Transactions.Should().HaveCount(2);
            result.TotalCredit.Should().Be(100);
            result.TotalDebit.Should().Be(50);
        }
        [Fact]
        public async Task GetStatement_ShouldReturnEmpty_WhenNoTransactions()
        {
            // Arrange
            var accountId = 1;
            var from = DateTime.UtcNow.AddDays(-5);
            var to = DateTime.UtcNow;

            _repoMock.Setup(r => r.GetTransactions(accountId, from, to, 0, 10))
                .ReturnsAsync(new List<Transaction>());

            _repoMock.Setup(r => r.GetSummary(accountId, from, to))
                .ReturnsAsync((0, 0));

            // Act
            var result = await _service.GetStatement(accountId, from, to, 1, 10);

            // Assert
            result.Transactions.Should().BeEmpty();
            result.TotalCredit.Should().Be(0);
            result.TotalDebit.Should().Be(0);
        }

        [Fact]
        public async Task GetStatement_ShouldThrowException_WhenDateRangeExceeds30Days()
        {
            // Arrange
            var accountId = 1;
            var from = DateTime.UtcNow.AddDays(-40);
            var to = DateTime.UtcNow;

            // Act
            Func<Task> act = async () =>
                await _service.GetStatement(accountId, from, to, 1, 10);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Max 30 days allowed");
        }
    }
}
