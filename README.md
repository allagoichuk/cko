# Payments

This solution implements payment processing prototype using .NET Core 2.2.

## Projects
* `Checkout.Payments.Api` implements REST endpoints using WebApi, uses Payments Processor to do the actual processing.

* `Checkout.Payments.Processor` implements logic of validating requests, persisting valid payment requests into Repository, requesting Bank to process payment and stores the result.

* `tests/Checkout.Payments.Api.UnitTests` implements unit test for Payments Api project. Not comprehensive, just an example.

* `tests/integration-tests.sh` bash script with curl requests used in docker-compose (see [below](#docker-compose)).

## Checkout.Payments.Api

Endpoints are:

* `POST api/Payments` accepts payment request.

* `GET api/Payments/{id}` returns status of payment request made earlier 

Addeded Swagger middleware. There is `/swagger` page available that describes API and provide simple in-browser interface to test it.

Implemented global exception handling middlware that wraps errors into structure similar to Checkout API.

For the lack of time did not implement authorisation middleware that inspects `X-API-Key` header and returns 401 if not present or incorrect. Token bearer or JWT might be an option but they more often used to represent end-user (customer). Api Key represents seller.

For the lack of time `Idempotency-key` is imeplemented not as header but as a field in POST request. It is extracted if present, and passed along with other data to Payments Processor.

## Checkout.Payments.Processor

### Idempotency
IdempotencyKey protects from charging multiple times. For example when client did not receive response for earlier payment request and does not know payment status and retries the request.

Similar to Checkout API, if payment with the same IdempotencyKey + other payment attributtes received and present in repository then the response for the stored payment is returned again (see `UniqueIdempotencyId` method).

### Bank
Payments Processor interacts with Bank via `IBankClient` interface. Implementations are injected dependencies. Implementation are chosen depending on BankProvider Type in _appsettings.json_.

* `MockBankClient` can be used in unit tests or for local testing. Is injected if value of BankProvider Type is `Mock`. Rejects any cards by defaut, exept those configured to accept, pending etc. See _appsettings.json_ for example.

* `RestBankClient` is not implemented for the lack of time.

### Repository
Injectable dependency. Current implementation is in-memory using  `ConcurrentDictionary`.

## Run locally
To run locally Debug version, run the following command then open in browser `http://localhost:59141/swagger` or use *curl* or *Postman* to test it

    dotnet run --project Checkout.Payments.Api

To run unit tests

    dotnet test

## Docker
I use multi-stage `Dockerfile` to buld Release version of Payments API using sdk image then copy artefacts to runtime image. Run the follwing command on any machine with Docker, for example Jenkins. No .NET Core requred

    docker build -t paymentsapi .

To run Payments API container locally, run the following then open in browser `http://localhost:8000/swagger`

    docker run --rm -it -p8000:80 paymentsapi

To log each request override *appsettings.json* logging level using environment variable

    docker run --rm -it -p8000:80 -e Logging__Console__LogLevel__Default=Information paymentsapi

## docker-compose

`docker-compose.yaml` demonstrates a composition where Payments API container is being hit externally from another container that runs simple bash script with curl commands, akin to integration test but not really tests as I don't examine response. Both containers configured to accept the same card number. To run use the follwing command

    docker-compose up --abort-on-container-exit

Need to use `--abort-on-container-exit` to stop the composition when curl script is finished. On the first run will build images for both containers
