## Get User Information

**Endpoint:** `/api/auth/me`

**Method:** `GET`

**Description:** Retrieves the authenticated user's information using a valid token and refresh token.

**Request Body:**
The request body should be a JSON object with the following fields:
- `Token` (string, required): The current JWT token.
- `RefreshToken` (string, required): The refresh token.

**Example Request:**
```json
{
  "Token": "current-jwt-token",
  "RefreshToken": "current-refresh-token"
}
```

**Response:**
- **Status Code:** `200 OK` - If the user information is retrieved successfully.
- **Status Code:** `404 Not Found` - If the user is not found or the refresh token is invalid.
- **Status Code:** `400 Bad Request` - If the refresh token is revoked.

**Example Response (Success):**
```json
{
  "id": "user-id",
  "email": "user@example.com",
  "firstName": "First",
  "lastName": "Last",
  "roles": ["role1", "role2"],
  "claims": [
    {
      "type": "claim-type",
      "value": "claim-value"
    }
  ]
}
```

**Possible Errors:**
- `user_not_found`: The user associated with the token was not found.
- `invalid_refresh_token`: The refresh token is invalid or not associated with the user.
- `refresh_token_revoked`: The refresh token has already been revoked.

**Error Responses:**

- **User Not Found:**
  - **Status Code:** `404 Not Found`
  - **Error Code:** `user_not_found`
  - **Message:** "User not found"

  **Example Response:**
  ```json
  {
    "title": "Not Found",
    "status": 404,
    "detail": "User not found",
    "errors": [
      {
        "code": "user_not_found",
        "message": "User not found"
      }
    ]
  }
  ```

- **Invalid Refresh Token:**
  - **Status Code:** `404 Not Found`
  - **Error Code:** `invalid_refresh_token`
  - **Message:** "Refresh token not found or invalid"

  **Example Response:**
  ```json
  {
    "title": "Not Found",
    "status": 404,
    "detail": "Refresh token not found or invalid",
    "errors": [
      {
        "code": "invalid_refresh_token",
        "message": "Refresh token not found or invalid"
      }
    ]
  }
  ```

- **Refresh Token Revoked:**
  - **Status Code:** `400 Bad Request`
  - **Error Code:** `refresh_token_revoked`
  - **Message:** "Refresh token already revoked"

  **Example Response:**
  ```json
  {
    "title": "Bad Request",
    "status": 400,
    "detail": "Refresh token already revoked",
    "errors": [
      {
        "code": "refresh_token_revoked",
        "message": "Refresh token already revoked"
      }
    ]
  }
  ```

**Notes:**
- The `Token` field should contain the current JWT token.
- The `RefreshToken` field should contain a valid refresh token.
- Upon successful retrieval, the user's information is returned.