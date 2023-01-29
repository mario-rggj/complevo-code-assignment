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

### TODO
- [ ] Finish README 
- [ ] Add query strings example to API
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
- [ ] pre push hook to run tests