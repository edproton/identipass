## Login User

**Endpoint:** `/api/auth/login`

**Method:** `POST`

**Description:** Authenticates a user and returns a JWT token and a refresh token.

**Request Body:**
The request body should be a JSON object with the following fields:
- `Username` (string, optional): The user's username.
- `Email` (string, required): The user's email address.
- `Password` (string, required): The user's password.

**Example Request:**
```json
{
  "Username": "user123",
  "Email": "user@example.com",
  "Password": "SecurePassword123"
}
```

**Response:**
- **Status Code:** `200 OK` - If the login is successful.
- **Status Code:** `400 Bad Request` - If there are validation errors (e.g., username or email is required).
- **Status Code:** `401 Unauthorized` - If the credentials are invalid.

**Example Response (Success):**
```json
{
  "token": "jwt-token",
  "refreshToken": "refresh-token"
}
```

**Example Response (Validation Error):**
```json
{
  "title": "Validation error",
  "status": 400,
  "detail": "Username or email is required",
  "errors": [
    {
      "code": "username_or_email_required",
      "message": "Username or email is required"
    }
  ]
}
```

**Example Response (Invalid Credentials):**
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Invalid credentials",
  "errors": [
    {
      "code": "invalid_credentials",
      "message": "Invalid credentials"
    }
  ]
}
```

**Possible Errors:**
- `username_or_email_required`: Either username or email is required.
- `invalid_credentials`: The provided credentials are invalid.

**Error Responses:**

- **Username or Email Required:**
  - **Status Code:** `400 Bad Request`
  - **Error Code:** `username_or_email_required`
  - **Message:** "Username or email is required"

  **Example Response:**
  ```json
  {
    "title": "Validation error",
    "status": 400,
    "detail": "Username or email is required",
    "errors": [
      {
        "code": "username_or_email_required",
        "message": "Username or email is required"
      }
    ]
  }
  ```

- **Invalid Credentials:**
  - **Status Code:** `401 Unauthorized`
  - **Error Code:** `invalid_credentials`
  - **Message:** "Invalid credentials"

  **Example Response:**
  ```json
  {
    "title": "Unauthorized",
    "status": 401,
    "detail": "Invalid credentials",
    "errors": [
      {
        "code": "invalid_credentials",
        "message": "Invalid credentials"
      }
    ]
  }
  ```

**Notes:**
- Either the `Username` or `Email` field must be provided to identify the user.
- A valid JWT token and refresh token are returned upon successful authentication.