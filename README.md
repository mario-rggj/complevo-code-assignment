# Complevo Code Assignment
## Starting the application
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

- **If you are running through an IDE or profile, check if the database container is up `docker-compose up db`** 

### Testing
```sh
dotnet test
```
- **The integration tests uses an SQLServer instance, so check if db container is up before running tests `docker-compose up db`**
---
## Project Details
### Layers
#### Interface
The interface layers' purpose is to isolate the presentation logic from the the rest of the codebase. Usually this layer would contemplate the part of the codebase responsible for rendering user interfaces, but since this project doesnt have user interfaces, it isolates the front facing outermost part of the codebase, in this case, the API endpoints and Controllers.

Components:
- Controllers
  - The Controllers are responsible for mapping endpoints routes, converting domain objects to HTTP responses.
- DTOs
  - The DTOs acts as ModelView for when requests body or responses slightly different than the Domain classes.

#### Application
The application layer is the layer between the API Interface layer, and the Domain layer. Its purpose is to decouple the Interface layer from the rest of the application, preventing the Interface layer from having direct access to the Domain and data manipulation services. It also coordinates interactions between Domain and Infrastructure classes, like persisting new Model instances.

Components:
- UseCases
  - The UseCases acts as application services, coordinating data manipulation and delegating persistence to the right place. Each use case is responsible for one feature, through the Interface Segregation principle, enabling all the features main characteristics to be avaiable at the same place, and isolated from other features.

#### Domain
The Domain layer is the core of the application. It isolates the domain representation classes, contracts, relationships, and logic from the other layers. 

Components:
- Interfaces for classes
  - The interfaces for the classes acts as an implementation of the Dependency Inversion principle. It makes implementation contracts available for classes in other layers of the codebase. 
- Domain Models
  - The Domain Models are where the domain representation classes lives. It is responsible for isolating the domain representations into classes and relationships and describing domain logic 
- Domain Exceptions
  - Domain Exceptions turns generic exceptions into domain specific exceptions, abstracting technical details and giving proper name and description for better understanding.

#### Infrastructure
This layer is responsible for isolating the data manipulation and persistence from the rest to the application, also acting as an anti corruption layer, hiding details of the data manipulation from the other layers and making it easier for future vendor change or data handling refactors.

Components:
- Repositories
  - The repositories acts as in memory persistence for transitory data before being committed to the persistent database
- Unit Of Work
  - It is responsible for making data manipulation through repositories available to the Use Cases, and commiting changes to the database. 
- Context
  - Its responsabilities includes creating connections to the database, configuring it and mapping domain classes relationships cardinality.  
- Migrations
  - The migrations enables the database schema to be versioned and to update the schema automatically based on that version

### Unit x Integration Tests
Since this initial state of the code assignment doesn't have too much business specific logic, unit tests wouldn't have much to test. Still, the application has CRUD operations to evaluate, so it made more sense to focus on integration tests.
The database provider used on the tests is the same used in development, but different schema, for more accurate test results.

### Why not Minimal API? Why not Dapper?
Short answer, personal preference. I've been developing in Node (TypeScript, Express, no ORM) for quite some time now and really missed writing code in a strong typed language with a more enterprise feel.
But on a real world application it wouldn't be a simple personal preference for me, it would depend on a lot of different variables, and of course the personal preference, but from the company and team.

---

### TODO
- [ ] Add Name and Description search filter to the get all endpoint
- [ ] Add sorting to the get all endpoint
- [x] Finish README 
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
- [ ] hide secrets
- [ ] pre commit hook to look for secrets (Talisman)
- [x] pre commit hook to lint staged files
- [x] pre commit hook to run tests