# StealAllTheCats

## Setup DB

1. Fill in the missing information in appsettings.json for the Connection String.
2. Open Package Manager Console and select project StealAllTheCats.Infrastructure.
3. Execute the command "update-database" to run the migration script to your DB.

## Run The Project

The solution is ready to use with Docker Linux. Execute run Container (Dockerfile).

## Architecture

A 3-layer architecture is being used (API, Domain-Business, Infrastructure-Repository).

## Features

1. Used dockerfile.
2. Implemented middleware to catch global exception (don't return error messages to API consumers).
