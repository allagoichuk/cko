curl -s -X POST $PAYMENTS_URL \
    -H "Content-Type: application/json" \
    -d "{ 'amount': 0, 
          'currency': 'string', 
          'cardNumber': 'string', 
          'expiryDate': 'string', 
          'cvv': 'string', 
          'idempotencyKey': 'string' }" && echo

curl -s -X POST $PAYMENTS_URL \
    -H "Content-Type: application/json" \
    -d "{ 'amount': 10, 
          'currency': 'string', 
          'cardNumber': 'string', 
          'expiryDate': 'string', 
          'cvv': 'string', 
          'idempotencyKey': 'string' }" && echo

curl -s -X POST $PAYMENTS_URL \
    -H "Content-Type: application/json" \
    -d "{ 'amount': 10, 
          'currency': 'USD', 
          'cardNumber': '$MockCardNumberAuthorized', 
          'expiryDate': '08/12', 
          'cvv': '123', 
          'idempotencyKey': 'string' }" && echo

curl -s -X POST $PAYMENTS_URL \
    -H "Content-Type: application/json" \
    -d "{ 'amount': 10, 
          'currency': 'USD', 
          'cardNumber': '4111111111111100', 
          'expiryDate': '08/12', 
          'cvv': '123', 
          'idempotencyKey': 'string' }" && echo

exit 0
