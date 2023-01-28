### Running
Deploy locally using docker
```sh
docker-compose up
```
Swagger avaiable at
```
http://localhost:8080/swagger/index.html
```

### Debugging
In order to debug, the database container must be online.

### TODO
- [ ] Finish README 
- [ ] Add query strings example to API
- [ ] Unit tests, at least 1
- [ ] Integration tests for endpoints
- [x] Use Docker
- [x] Implement Repository Pattern
- [x] Use Separation of Concerns
- [x] Apply Dependency Inversion Principle

#### Bonus TODO
- [ ] Add Patch method for Product
- [ ] Create custom exception for entity not found
- [ ] Automated deploy with GitHub Actions + AWS or Serverless Framework + localstack
- [ ] pre commit hook to lint staged files
- [ ] pre push hook to run tests