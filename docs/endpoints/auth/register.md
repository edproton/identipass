## Register User

**Endpoint:** `/api/auth/register`

**Method:** `POST`

**Description:** Registers a new user in the system. This endpoint requires a minimum of email, password, and confirm password fields. An optional username field can also be provided.

**Required Fields:**
- `Email` (string): The user's email address.
- `Password` (string): The user's password.
- `ConfirmPassword` (string): The confirmation of the user's password.

**Optional Fields:**
- `Username` (string): The user's username.

**Example Request:**
```json
{
  "Email": "user@example.com",
  "Password": "password123",
  "ConfirmPassword": "password123"
}
```

**Response:**
- **Status Code:** `204 No Content` - If the registration is successful.

**Errors:**

- **Password Mismatch:**
    - **Status Code:** `400 Bad Request`
    
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "passwords_do_not_match",
        "message": "Passwords do not match",
        "type": "Validation"
      }
    ]
  }
  ```

- **Email Already Taken:**
  - **Status Code:** `409 Conflict`

  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "email_already_taken",
        "message": "Email already taken",
        "type": "Conflict"
      }
    ]
  }
  ```

- **Username Already Taken:**
  - **Status Code:** `409 Conflict`
  ```json
  {
    "errors": [
      {
        "code": "username_already_taken",
        "message": "Username already taken",
        "type": "Conflict"
      }
    ]
  }
  ```