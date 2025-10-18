# WorkTracker

WorkTracker is a web application for employees to track daily work hours, descriptions, and associated clients. It allows the employer to see hours worked per client, calculate total hours and wages over a selected date rangen

---

## Features

- **Daily Work Logging:**  
  Employees can add daily work entries including:
  - Hours worked
  - Description of work
  - Client associated with the work

- **Date Range Summary:**  
  Select a date range using the date picker to calculate:
  - Total hours worked
  - Total wage earned for that period based on configured hourly rates

- **Client Tracking:**  
  Each work entry is linked to a specific client to facilitate billing.

- **Automatic Wage Calculation:**  
  Calculates total earnings using the hourly rate configured.

- **Four Functional Tabs:**
  1. **Add Work:** Enter new work entries with hours, description, and client.
  2. **Work Items Grid:** View a searchable grid of all work entries. You can edit, delete, or view work entry details.
  3. **Clients:** Manage clients by adding, editing, or removing them.
  4. **Work Rates:** Manage hourly rates by adding, editing, or removing rates.

---

## Tech Stack

- **Backend:** .NET Web API  
- **Frontend:** Vue.js  
- **API Endpoints:** RESTful endpoints for CRUD operations on work entries, clients, and work rates.
