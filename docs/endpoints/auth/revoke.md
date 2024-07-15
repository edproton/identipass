# endpoints/auth_revoke.md

## Revoke Refresh Token

**Endpoint:** `/api/auth/revoke`

**Method:** `POST`

**Description:** Revokes a refresh token for a user. This endpoint requires the user ID and the refresh token to be revoked.

**Notes:**
- The `UserId` field should contain the user's unique identifier.
- The `RefreshToken` field should contain the refresh token to be revoked.
- Upon successful revocation, the refresh token will be marked as revoked and will no longer be usable.

**Required Fields:**
- `UserId` (Guid): The user's ID.
- `RefreshToken` (string): The refresh token to be revoked.

**Example Request:**
```json
{
  "UserId": "4ef1c711-06ff-41ea-888a-456579e447d6",
  "RefreshToken": "bSiwLoqiZF0ZatZpNHoDeEntfvyT4GZWRWAr0u1r7NNxeeWzMph7kDbWzi2kgyM+QYleL/F8U5JniSyHQEdTEg=="
}
```

**Response:**
- **Status Code:** `200 OK` - If the refresh token is successfully revoked.

**Errors:**

- **Refresh Token Not Found:**
  - **Status Code:** `404 Not Found`
  
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "refresh_token_not_found",
        "message": "Refresh token not found",
        "type": "NotFound"
      }
    ]
  }
  ```

- **Revoked Refresh Token:**
  - **Status Code:** `404 Not Found`
  
  **Response:**
  ```json
  {
    "errors": [
      {
        "code": "refresh_token_revoked",
        "message": "Refresh token already revoked",
        "type": "NotFound"
      }
    ]
  }
  ```