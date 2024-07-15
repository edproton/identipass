## User Login

**Endpoint:** `/api/auth/login`

**Method:** `POST`

**Description:** Authenticates a user using their username/email and password, and returns a JWT token and refresh token.


**Required Fields:**
- `usernameOrEmail` (string): The user's username or email address.
- `password` (string): The user's password.

**Example Request:**
```json
{
  "usernameOrEmail": "teste",
  "password": "12345"
}
```

**Response:**
- **Status Code:** `200 OK` - If the login is successful.

**Example Response:**
```json
{
  "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0ZWYxYzcxMS0wNmZmLTQxZWEtODg4YS00NTY1NzllNDQ3ZDYiLCJuYmYiOjE3MjEwNzgyODQsImV4cCI6MTcyMTY4MzA4NCwiaWF0IjoxNzIxMDc4Mjg0fQ.3o8BTcmIGL_6u_V1JZKI0n-LI8NGgAGiszEcZeZtRkZWGxVBdnPbyi8kSAm4w7YyIb4s_74G3k0LMUj2dP_JZQ",
  "refreshToken": "k5XNdpK+DPEeOPmGfB4h5q5kY+YJFXjbsnRFUbBqYySCpujm7L7szEu3YDOYRR4RnGWrZa9NNMTaLU0lytJMEQ=="
}
```

**Errors:**

- **Invalid Credentials:**
  - **Status Code:** `400 Bad Request`
  
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "invalid_credentials",
        "message": "Invalid credentials",
        "type": "Validation"
      }
    ]
  }
  ```