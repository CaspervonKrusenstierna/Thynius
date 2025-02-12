#!/bin/sh

# Start Minio server in the background
/bin/minio server /data &

# Wait a bit for Minio to fully initialize
sleep 10

# Set up the Minio Client (mc)
mc alias set myminio http://localhost:9000 ${MINIO_ACCESS_KEY} ${MINIO_SECRET_KEY}

# Create the 'thynius' bucket under the 'myminio' alias
mc mb data/thynius

# Apply the CORS configuration to the 'thynius' bucket under the 'myminio' alias
mc cors set data/thynius - <<EOF
<?xml version="1.0" encoding="UTF-8"?>
<CORSConfiguration>
    <CORSRule>
        <AllowedOrigin>*</AllowedOrigin>
        <AllowedMethod>GET</AllowedMethod>
        <AllowedMethod>POST</AllowedMethod>
        <AllowedMethod>PUT</AllowedMethod>
        <AllowedHeader>*</AllowedHeader>
        <ExposeHeader>x-amz-request-id</ExposeHeader>
        <ExposeHeader>x-amz-id-2</ExposeHeader>
        <MaxAgeSeconds>3000</MaxAgeSeconds>
    </CORSRule>
</CORSConfiguration>
EOF

# Keep the container running
wait