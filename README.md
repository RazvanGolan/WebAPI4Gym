# WebAPI4Gym

This is a dotnet web API project designed to manage gym members and coaches. It provides full CRUD (Create, Read, Update, Delete) operations and handles the relationships between members and coaches.

# Features
  <li>Member Management: Add, view, update, and delete gym members.</li>
  <li>Coach Management: Add, view, update, and delete gym coaches.</li>
  <li>Member-Coach Relationships: Assign a coach to a member and manage the list of members for each coach.</li>

# Technical details
- Technology Stack: .NET Core 7, ASP.NET Core 7, Entity Framework Core, PostgreSQL, Swagger UI
<details>
<summary>Project Structure</summary>
<ul>
  <li><strong>Controllers:</strong> Handle incoming HTTP requests and send responses.</li>
  <li><strong>Entities:</strong> Define the data models.</li>
  <li><strong>Repositories:</strong> Handle data access logic.</li>
</ul>
</details>

# API endpoints

![Screenshot 2024-06-01 at 08 23 17](https://github.com/RazvanGolan/WebAPI4Gym/assets/117024228/dbfc4bb2-2f9b-4565-ac56-4d51025e8e57)

# Data Models
<details>
<summary>Member</summary>
<ul>
  <li><strong>FirstName:</strong> The first name of the member.</li>
  <li><strong>LastName:</strong> The last name of the member.</li>
  <li><strong>Created:</strong> The date and time when the member was added to the system.</li>
  <li><strong>GoldenState:</strong> A boolean indicating if the member has a special status (e.g., premium membership).</li>
  <li><strong>Email:</strong> The email address of the member.</li>
  <li><strong>Coach:</strong> The coach assigned to the member (if any).</li>
</ul>
</details>
<details>
<summary>Coach</summary>
<ul>
  <li><strong>FirstName:</strong> The first name of the coach.</li>
  <li><strong>LastName:</strong> The last name of the coach.</li>
  <li><strong>Created:</strong> The date and time when the coach was added to the system.</li>
  <li><strong>MemberList:</strong> A list of members assigned to the coach.</li>
  <li><strong>Limit:</strong> The maximum number of members a coach can manage.</li>
</ul>
</details>
