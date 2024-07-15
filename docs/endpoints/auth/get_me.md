## Get Me

**Endpoint:** `/api/auth/me`

**Method:** `GET`

**Description:** Retrieves the authenticated user's information based on the provided access token and refresh token.

**Headers:**
- `Authorization` (string): The user's access token.
- `RefreshToken` (string): The user's refresh token.

**Example Request:**
```http
GET /api/auth/me
Authorization: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0ZWYxYzcxMS0wNmZmLTQxZWEtODg4YS00NTY1NzllNDQ3ZDYiLCJuYmYiOjE3MjEwNzgyODQsImV4cCI6MTcyMTY4MzA4NCwiaWF0IjoxNzIxMDc4Mjg0fQ.3o8BTcmIGL_6u_V1JZKI0n-LI8NGgAGiszEcZeZtRkZWGxVBdnPbyi8kSAm4w7YyIb4s_74G3k0LMUj2dP_JZQ
RefreshToken: k5XNdpK+DPEeOPmGfB4h5q5kY+YJFXjbsnRFUbBqYySCpujm7L7szEu3YDOYRR4RnGWrZa9NNMTaLU0lytJMEQ==
```

**Example Response:**
```json
{
  "id": "4ef1c711-06ff-41ea-888a-456579e447d6",
  "email": "teste@gmail.com",
  "username": "teste",
  "roles": [],
  "claims": []
}
```

**Response:**
- **Status Code:** `200 OK` - If the retrieval is successful.

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