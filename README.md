# 📊 Statement Service (ASP.NET Core Web API)

## 📌 Project Overview
This is a backend REST API built using **ASP.NET Core Web API** that generates account statements based on transaction data.

It demonstrates:
- Clean Architecture
- Repository Pattern
- Unit Testing (xUnit + Moq)
- Business logic separation

---

## 🏗️ Architecture

The solution follows **Clean Architecture principles**:
## ⚙️ Features

- Get account statement by account ID
- Filter transactions by date range
- Pagination support (page number & page size)
- Calculate total credit & debit
- Date validation (maximum 30 days range)
- Exception handling for invalid inputs

---

## 🧪 Unit Testing

Unit tests are written using:

- xUnit
- Moq (mocking dependencies)
- FluentAssertions (clean assertions)

### ✔ Test Cases Covered

- Valid transactions returned successfully
- Empty transaction scenario handled
- Date range validation (max 30 days rule)
- Repository layer is properly mocked

--- 


## 🛠️ Tech Stack

- .NET 8
- ASP.NET Core Web API
- xUnit
- Moq
- FluentAssertions