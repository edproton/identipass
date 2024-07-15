# endpoints/auth_refresh.md

## Refresh Token

**Endpoint:** `/api/auth/refresh`

**Method:** `POST`

**Description:** Refreshes the authentication tokens (access token and refresh token) for a user. This endpoint requires a valid access token and refresh token. The old token will be revoked after the refresh.

**Notes:**
- The `Token` field should contain the current JWT token.
- The `RefreshToken` field should contain a valid refresh token.
- Upon successful refresh, a new JWT token and refresh token are returned.

**Required Fields:**
- `Token` (string): The user's current access token.
- `RefreshToken` (string): The user's current refresh token.

**Example Request:**
```json
{
  "Token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0ZWYxYzcxMS0wNmZmLTQxZWEtODg4YS00NTY1NzllNDQ3ZDYiLCJuYmYiOjE3MjEwNzg2NDksImV4cCI6MTcyMTY4MzQ0OSwiaWF0IjoxNzIxMDc4NjQ5fQ.GUqoIBU9jR3eeZLev2pXQwh2DoQeZwKg1Vhoj1KKEED5E82RkUSLCwFDgns3rxZQHgENPTSQc4AKYSV0ELVH-A",
  "RefreshToken": "bSiwLoqiZF0ZatZpNHoDeEntfvyT4GZWRWAr0u1r7NNxeeWzMph7kDbWzi2kgyM+QYleL/F8U5JniSyHQEdTEg=="
}
```

**Response:**
- **Status Code:** `200 OK` - If the tokens are successfully refreshed.

**Example Response:**
```json
{
  "Token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0ZWYxYzcxMS0wNmZmLTQxZWEtODg4YS00NTY1NzllNDQ3ZDYiLCJuYmYiOjE3MjEwNzg3NjksImV4cCI6MTcyMTY4MzU2OSwiaWF0IjoxNzIxMDc4NzY5fQ.2mxqDEoMr0uahxANc2vnVmvuyqhvwY3MLRtdxrvsAFeaVc6OjdQIlGZGn4gkvgvZ11Y8BWRLx1pEpP_-fDKggg",
  "RefreshToken": "WJ2gJGMQ74PZORWUAYz0FVv+hsnEW0wxI7bY/JkbRTPq5POZg26Zlitl44sf33KBqyGmeQOgBw5KfvFvnLlv5Q=="
}
```

**Errors:**

- **User Not Found:**
  - **Status Code:** `404 Not Found`
  
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "user_not_found",
        "message": "User not found",
        "type": "NotFound"
      }
    ]
  }
  ```

- **Invalid Refresh Token:**
  - **Status Code:** `404 Not Found`
  
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "invalid_refresh_token",
        "message": "Invalid refresh token",
        "type": "NotFound"
      }
    ]
  }
  ```