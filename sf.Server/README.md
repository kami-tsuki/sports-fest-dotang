## Endpoints

### BaseController<T> Overview

The `BaseController<TEntity>` in our .NET API provides a set of generic endpoints for managing entities. This controller includes CRUD operations and additional functionalities such as bulk operations, search, and export. Below is a detailed description of each endpoint and its usage.

#### Basic Endpoints

1. **GET /api/v1/<T>/base**
    - **Description**: Retrieves a paginated list of entities.
    - **Parameters**:
        - `page` (optional): The page number to retrieve (default is 1).
        - `entities` (optional): The number of entities per page (default is 10).
        - `properties` (optional): A comma-separated list of properties to include in the response.
        - `sendNull` (optional): Whether to include null properties in the response.
        - `filters` (optional): A dictionary of filters to apply to the query.
    - **Response**: A paginated list of entities.

2. **POST /api/v1/<T>/base/bulk**
    - **Description**: Creates multiple entities in bulk.
    - **Request Body**: An array of entities to create.
    - **Response**: The created entities.

3. **PUT /api/v1/<T>/base/bulk**
    - **Description**: Updates multiple entities in bulk.
    - **Request Body**: An array of entities to update.
    - **Response**: The updated entities.

4. **DELETE /api/v1/<T>/base/bulk**
    - **Description**: Deletes multiple entities in bulk.
    - **Request Body**: An array of entity IDs to delete.
    - **Response**: The IDs of the deleted entities.

5. **GET /api/v1/<T>/base/search**
    - **Description**: Searches for entities based on a query string.
    - **Parameters**:
        - `query`: The search query.
        - `page` (optional): The page number to retrieve (default is 1).
        - `entities` (optional): The number of entities per page (default is 10).
        - `properties` (optional): A comma-separated list of properties to include in the response.
        - `sendNull` (optional): Whether to include null properties in the response.
        - `filters` (optional): A dictionary of filters to apply to the query.
    - **Response**: A paginated list of entities matching the search query.

6. **GET /api/v1/<T>/base/count**
    - **Description**: Retrieves the count of entities.
    - **Parameters**:
        - `filters` (optional): A dictionary of filters to apply to the query.
    - **Response**: The count of entities.

7. **GET /api/v1/<T>/base/{id}**
    - **Description**: Retrieves a single entity by its ID.
    - **Parameters**:
        - `id`: The ID of the entity to retrieve.
        - `properties` (optional): A comma-separated list of properties to include in the response.
    - **Response**: The entity with the specified ID.

8. **POST /api/v1/<T>/base**
    - **Description**: Creates a new entity.
    - **Request Body**: The entity to create.
    - **Parameters**:
        - `ignoreNullProperties` (optional): Whether to ignore null properties during creation.
    - **Response**: The created entity.

9. **PUT /api/v1/<T>/base/{id}**
    - **Description**: Updates an existing entity by its ID.
    - **Parameters**:
        - `id`: The ID of the entity to update.
        - `ignoreNullProperties` (optional): Whether to ignore null properties during the update.
    - **Request Body**: The updated entity.
    - **Response**: No content.

10. **PATCH /api/v1/<T>/base/{id}**
    - **Description**: Partially updates an existing entity by its ID.
    - **Parameters**:
        - `id`: The ID of the entity to update.
    - **Request Body**: A JSON Patch document describing the changes.
    - **Response**: No content.

11. **DELETE /api/v1/<T>/base/{id}**
    - **Description**: Deletes an entity by its ID.
    - **Parameters**:
        - `id`: The ID of the entity to delete.
    - **Response**: The deleted entity.

12. **OPTIONS /api/v1/<T>/base**
    - **Description**: Retrieves the allowed HTTP methods for the controller.
    - **Parameters**:
        - `path` (optional): The specific path to get options for.
        - `includeModels` (optional): Whether to include model information in the response.
        - `includeHttpCodes` (optional): Whether to include HTTP status codes in the response.
    - **Response**: The allowed HTTP methods, models, and HTTP status codes.

13. **GET /api/v1/<T>/base/export**
    - **Description**: Exports entities in the specified format.
    - **Parameters**:
        - `format` (optional): The export format (default is "csv").
        - `filters` (optional): A dictionary of filters to apply to the query.
    - **Response**: The exported data in the specified format.

14. **POST /api/v1/<T>/base/validate**
    - **Description**: Validates an entity.
    - **Request Body**: The entity to validate.
    - **Response**: The validation result.

15. **GET /api/v1/<T>/base/{id}/audit**
    - **Description**: Retrieves the audit history for an entity by its ID.
    - **Parameters**:
        - `id`: The ID of the entity to retrieve audit history for.
    - **Response**: The audit history of the entity.

These endpoints provide a comprehensive set of operations for managing entities in a consistent and reusable manner.

## Result Models

### ResultModel\<T\> Overview

The `ResultModel<T>` is a standardized response model used by our API to encapsulate the results of API operations. This model provides a consistent structure for both successful and error responses, making it easier for API users to handle and interpret the results.

#### JSON Structure

The JSON response from the API using `ResultModel<T>` will have the following structure:

```json
{
  "success": true,
  "error": false,
  "message": "Operation completed successfully.",
  "data": {
    // Entity-specific data: <T>
  },
  "messages": [
    {
      "error.message": "An error occurred.",
      "error.type": 1,
      "error.severity": 4
    }
  ]
}
```

#### Fields

1. **success** (boolean)
    - Indicates whether the operation was successful.
    - `true` if the operation was successful, `false` otherwise.

2. **error** (boolean)
    - Indicates whether there was an error.
    - `true` if there was an error, `false` otherwise.
    - This field is derived from the `success` field (`error` is `!success`).

3. **message** (string)
    - Provides a human-readable message about the result of the operation.
    - Default value is "An error occurred." for error responses.

4. **data** (<T>)
    - Contains the data specific to the entity being operated on.
    - This field will be populated with the relevant entity data for successful operations.

5. **messages** (array of Message)
    - Contains detailed error messages if the operation failed.
    - Each message includes:
        - **error.message** (string): A description of the error.
        - **error.type** (integer): An enumeration value representing the type of error.
        - **error.severity** (integer): An enumeration value representing the severity of the error.

#### Example Usage

**Successful Response:**

```json
{
  "success": true,
  "error": false,
  "message": "Entity retrieved successfully.",
  "data": {
    "id": "123",
    "name": "Sample Entity",
    "createdAt": "2023-10-01T12:00:00Z",
    "updatedAt": "2023-10-01T12:00:00Z"
  },
  "messages": []
}
```

**Error Response:**

```json
{
  "success": false,
  "error": true,
  "message": "Validation failed.",
  "data": null,
  "messages": [
    {
      "error.message": "The 'name' field is required.",
      "error.type": 2,
      "error.severity": 4
    }
  ]
}
```

### Error Types and Severities

- **Error Types** (`ErrorResult` enum):
    - `1`: InternalException
    - `2`: InvalidModel
    - `3`: InvalidCredentials
    - `4`: InvalidToken
    - `5`: InvalidRole
    - `6`: InvalidUser
    - `7`: InvalidPassword
    - `8`: InvalidEmail
    - `9`: InvalidUsername
    - `10`: InvalidOldPassword
    - `11`: InvalidNewPassword
    - `12`: InvalidModelState
    - `13`: PasswordChangeFailed
    - `14`: AccountLocked
    - `15`: UserNotFound
    - `16`: UserCreationFailed
    - `17`: RoleCreationFailed
    - `18`: Validation
    - `19`: InvalidId
    - (More could be coming)

- **Severities** (`Severity` enum):
    - `0`: Verbose
    - `1`: Debug
    - `2`: Information
    - `3`: Warning
    - `4`: Error
    - `5`: Fatal

### Pagination in List Returning Endpoints

In our API, list returning endpoints utilize pagination to manage large sets of data efficiently. The pagination mechanism is implemented using the `Page<TEntity>` class, which encapsulates the paginated data along with metadata about the pagination state. The paginated results are always returned within a `ResultModel<Page<TEntity>>` to provide a consistent response structure.

#### How Pagination Works

When you request a list of entities from the API, you can specify pagination parameters to control the number of entities returned and the page of results you want to retrieve. The key parameters for pagination are:

- `page`: The page number to retrieve (default is 1).
- `entities`: The number of entities per page (default is 10).

The API processes these parameters and returns a `Page<TEntity>` object wrapped in a `ResultModel`. The `Page<TEntity>` object contains the following fields:

- `number`: The current page number.
- `size`: The number of entities in the current page.
- `total`: The total number of entities available.
- `data`: The list of entities in the current page.
- `total.pages`: The total number of pages available (calculated as `total / size`).

#### Example Response

Here is an example of how the paginated response looks in JSON format:

```json
{
  "success": true,
  "error": false,
  "message": "Entities retrieved successfully.",
  "data": {
    "number": 1,
    "size": 10,
    "total": 100,
    "data": [
      {
        "id": "123",
        "name": "Sample Entity",
        "createdAt": "2023-10-01T12:00:00Z",
        "updatedAt": "2023-10-01T12:00:00Z"
      },
      // More entities.. of type TEntity
    ],
    "total.pages": 10
  },
  "messages": []
}
```

In this example:
- The `number` field indicates that the first page of results is being returned.

