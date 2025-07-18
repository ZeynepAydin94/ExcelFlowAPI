# ExcelFlow 

ExcelFlow is a modern data processing platform built with React and .NET 9. It uploads large Excel files directly to Amazon S3, enqueues them via RabbitMQ, and processes them in the background using a .NET Worker Service. With dynamic validation rules and flexible table mappings, it processes data securely and efficiently.

## 🚀 Features

- 🔄 Upload Excel files directly to S3 using Pre-Signed URLs
- 🐇 Asynchronous processing via RabbitMQ
- 🛠️ Dynamic table and column mapping
- ✅ Template-based data validation
- 🧰 Built with .NET 9 API and Worker Service
- ⚛️ User-friendly frontend powered by React 18.3

## 🧱 Architecture

ExcelFlow has a scalable and modular architecture. Users upload Excel files via the React UI. A **pre-signed URL** is generated through the .NET API, allowing the file to be uploaded directly to **Amazon S3**. Once the upload is complete, the file metadata is sent to a **RabbitMQ queue**.

The **.NET Worker Service** listens to the queue. When a message arrives, it downloads the file from S3, validates each row using the appropriate **template**, and saves valid data to the database. Invalid rows are logged or preserved for reporting.
