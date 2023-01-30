### Prerequisites
- Docker
- .NET 7.0
- Node (only for linting purposes)
### Setup
```sh
# to install linting dependencies
npm install

# to install dotnet dependencies
dotnet restore
```

### Running
Deploy locally using docker
```sh
docker-compose up
```
Swagger available at [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

- **If you are running through an IDE or profile, check if the database container is up** 

### Testing
```sh
dotnet test
```
- **The integration tests uses an SQLServer instance, so check if db container is up before running tests**

### TODO
- [ ] Finish README 
- [ ] Add Name and Description search filter to the get all endpoint
- [ ] Add sorting to the get all endpoint
- [x] Add query strings example to API
- [x] Integration tests for endpoints
- [x] Use Docker
- [x] Implement Repository Pattern
- [x] Use Separation of Concerns
- [x] Apply Dependency Inversion Principle

#### Bonus TODO
- [ ] Add Patch method for Product
- [ ] Create custom exception for entity not found
- [ ] Automated deploy with GitHub Actions + AWS or Serverless Framework + localstack
- [x] pre commit hook to lint staged files
- [x] pre commit hook to run tests