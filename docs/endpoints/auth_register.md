## Register User

**Endpoint:** `/api/auth/register`

**Method:** `POST`

**Description:** Registers a new user with the provided details.

**Request Body:**
The request body should be a JSON object with the following fields:
- `Email` (string, required): The user's email address.
- `Password` (string, required): The user's password.
- `ConfirmPassword` (string, required): The password confirmation, which should match the password.
- `Username` (string, optional): The user's chosen username.
- `FirstName` (string, optional): The user's first name.
- `LastName` (string, optional): The user's last name.

**Example Request:**
```json
{
  "Email": "user@example.com",
  "Password": "SecurePassword123",
  "ConfirmPassword": "SecurePassword123",
  "Username": "user123",
  "FirstName": "John",
  "LastName": "Doe"
}
```

**Response:**
- **Status Code:** `201 Created` - If the registration is successful.
- **Status Code:** `400 Bad Request` - If there are validation errors (e.g., passwords do not match, email is already in use).
- **Status Code:** `409 Conflict` - If the email provided is already registered.

**Example Response (Success):**
```json
{
  "message": "User registered successfully",
  "userId": "guid-of-the-user"
}
```

**Example Response (Validation Error):**
```json
{
  "title": "Validation error",
  "status": 400,
  "detail": "Passwords do not match",
  "errors": [
    {
      "code": "passwords_do_not_match",
      "message": "Passwords do not match"
    }
  ]
}
```

**Example Response (Email Already Exists):**
```json
{
  "title": "Conflict",
  "status": 409,
  "detail": "The email 'user@example.com' is already registered",
  "errors": [
    {
      "code": "email_already_exists",
      "message": "The email 'user@example.com' is already registered"
    }
  ]
}
```

**Possible Errors:**
- `passwords_do_not_match`: Password and ConfirmPassword do not match.
- `username_or_email_required`: Either username or email is required.
- `email_already_exists`: The email provided is already registered.

**Error Responses:**

- **Passwords Do Not Match:**
  - **Status Code:** `400 Bad Request`
  - **Error Code:** `passwords_do_not_match`
  - **Message:** "Passwords do not match"

  **Example Response:**
  ```json
  {
    "title": "Validation error",
    "status": 400,
    "detail": "Passwords do not match",
    "errors": [
      {
        "code": "passwords_do_not_match",
        "message": "Passwords do not match"
      }
    ]
  }
  ```

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

- **Email Already Exists:**
  - **Status Code:** `409 Conflict`
  - **Error Code:** `email_already_exists`
  - **Message:** "The email 'user@example.com' is already registered"

  **Example Response:**
  ```json
  {
    "title": "Conflict",
    "status": 409,
    "detail": "The email 'user@example.com' is already registered",
    "errors": [
      {
        "code": "email_already_exists",
        "message": "The email 'user@example.com' is already registered"
      }
    ]
  }
  ```

**Notes:**
- No token is generated upon registration.
- Future versions will include a confirmation email sent to the registered email address.