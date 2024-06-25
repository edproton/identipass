## Refresh Authentication Tokens

**Endpoint:** `/api/auth/refresh`

**Method:** `POST`

**Description:** Refreshes the authentication tokens (JWT and refresh token) using a valid refresh token.

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
- **Status Code:** `200 OK` - If the tokens are refreshed successfully.
- **Status Code:** `404 Not Found` - If the refresh token is invalid or not found.
- **Status Code:** `400 Bad Request` - If the refresh token is revoked.

**Example Response (Success):**
```json
{
  "token": "new-jwt-token",
  "refreshToken": "new-refresh-token"
}
```

**Possible Errors:**
- `user_not_found`: The user associated with the token was not found.
- `refresh_token_not_found`: The refresh token is invalid or not found.
- `refresh_token_revoked`: The refresh token has already been revoked.

**Error Responses:**

- **User Not Found:**
  - **Status Code:** `404 Not Found`
  - **Error Code:** `user_not_found`
  - **Message:** "User not found"

- **Refresh Token Not Found:**
  - **Status Code:** `404 Not Found`
  - **Error Code:** `refresh_token_not_found`
  - **Message:** "Refresh token not found"

- **Refresh Token Revoked:**
  - **Status Code:** `400 Bad Request`
  - **Error Code:** `refresh_token_revoked`
  - **Message:** "Refresh token already revoked"

**Notes:**
- The `Token` field should contain the current JWT token.
- The `RefreshToken` field should contain a valid refresh token.
- Upon successful refresh, a new JWT token and refresh token are returned.