Software Requirements Document
BrightPath: Student Scholarship Application Management System
Student Name: NDARUHUTSE Moise
ID: 28340
Client: Bright Future Foundation
Date: 7 November 2026
1. Introduction
1.1 Purpose
This document describes the functional requirements of BrightPath, a web-based system that lets
university students apply for scholarships offered by the Bright Future Foundation and lets
administrators review those applications and record decisions. It is written to give the developer
and the client the same clear understanding of what the system must do.
1.2 Scope
BrightPath covers student signup and login, scholarship application submission and tracking,
scholarship category management, application review, and reporting and filtering. It does not
cover payment of scholarships, email notifications, or any feature not listed in this document.
1.3 Definitions
• Student: a university student who uses the system to apply for a scholarship.
• Administrator: a Foundation staff member who manages categories and reviews
applications.
• Application status: Pending, Approved, or Rejected.
• Average score: the student's current average mark out of 20.
2. Overall Description
2.1 Problem Statement
The Foundation needs one place to receive scholarship applications, check that applicants meet
the conditions, and record the decision for each application. Managing this by hand is slow and
makes it easy to lose or mix up applications.
2.2 Users of the System
• Student: registers, logs in, applies, and follows the status of their own applications.
• Administrator: manages scholarship categories, reviews applications, changes their status,
and views reports.
2.3 Operating Environment
The system runs as a web application with a relational database and is used through a web
browser.
2.4 Assumptions and Constraints
• Scholarship categories are stored in the database and are not hard-coded.
• Only the features requested in the case study are implemented.
• The system is built without internet access and without AI tools, as required by the case study.
3. Functional Requirements
3.1 Authentication
FR-1  The system shall let a new student sign up using an email address and a password.
FR-2  The system shall let a registered student log in using their email and password.
FR-3  The system shall hash every password before storing it in the database.
FR-4  The system shall reject a login when the email or password is wrong and show an error
message.
FR-5  The system shall only allow logged-in users to open the application pages. A user who is
not logged in shall be sent to the login page.
FR-6  The system shall let a logged-in user log out.
3.2 Student: Submit an Application
FR-7  The system shall let a logged-in student submit a scholarship application.
FR-8  The application form shall collect: student ID, full name, email address, department, current
semester, current average score, monthly family income, scholarship category, and a
brief reason why the student needs the scholarship.
FR-9  The system shall not accept an application if any required field is empty.
FR-10  The system shall reject an application when the current average score is below 14/20 and
show a message explaining why.
FR-11  The system shall reject an application when the student is in semester 1 and show a
message explaining why. Only semester 2 and above can apply.
FR-12  The system shall require the student to select an available scholarship category.
FR-13  The system shall load the list of scholarship categories from the database, not from fixed
text.
FR-14  The system shall save every new application with the status Pending.
3.3 Student: Manage Own Applications
FR-15  The system shall let a student view the list of their own submitted applications.
FR-16  The system shall show the status (Pending, Approved, or Rejected) of each application.
FR-17  The system shall show a student only their own applications and never those of other
students.
FR-18  The system shall let a student update the details of an application while its status is
Pending.
FR-19  The system shall apply the same eligibility checks (FR-9 to FR-12) when an application is
updated.
FR-20  The system shall let a student withdraw (delete) an application while its status is Pending.
FR-21  The system shall not allow a student to update or delete an application once it is Approved
or Rejected.
3.4 Administrator: Scholarship Categories
FR-22  The system shall let the administrator create a scholarship category with a name and a
description.
FR-23  The system shall let the administrator update the name and description of a category.
FR-24  The system shall let the administrator delete a category.
FR-25  The system shall make new and changed categories appear in the student application form
without any change to the program code.
3.5 Administrator: Review Applications
FR-26  The system shall let the administrator view all applications submitted by students.
FR-27  The system shall let the administrator open one application and see its full details,
including the student information and the values used for the eligibility checks.
FR-28  The system shall let the administrator change the status of an application to Approved or
Rejected.
FR-29  The system shall show the updated status to the student the next time they view their
applications.
3.6 Administrator: Reporting and Filtering
FR-30  The system shall let the administrator view applications filtered by scholarship category.
FR-31  The system shall let the administrator view all Approved applications.
FR-32  The system shall let the administrator view all Rejected applications.
FR-33  The system shall let the administrator view applications filtered by department.
FR-34  The system shall let the administrator view applications filtered by current average score.
3.7 Access Control
FR-35  The system shall separate the student pages from the administrator pages, so a student
cannot open an administrator page.
FR-36  The system shall prevent a student from reading another student's application, even by
typing a different application ID into the address bar.
4. Data Requirements
The system shall store its data in a relational database with the following tables:
• STUDENT: student_ID (primary key), email, password (hashed).
• SCHOLARSHIP_CATEGORY: scholarship_ID (primary key), scholarship_name,
scholarship_description.
• SCHOLARSHIP_APPLICATION: application_ID (primary key), student_ID (foreign key),
full_name, department, semester, current_average_score, monthly_family_income,
scholarship_ID (foreign key), reason, status.
One student can have many applications, and one scholarship category can have many
applications. Each application belongs to exactly one student and one category.
5. Other Requirements
• Security: passwords are hashed, and each student can only reach their own data.
• Usability: every form and page is linked from a menu or button and works without typing
addresses by hand. Error messages say clearly what the user must fix.
• Reliability: eligibility rules are checked by the system itself, not left to the user.
6. Summary of Use Cases
• Student signs up and logs in.
• Student submits an application.
• Student views the status of applications.
• Student updates or withdraws a pending application.
• Administrator manages scholarship categories.
• Administrator reviews an application and approves or rejects it.
• Administrator filters applications by category, status, department, or average score.
========================-----------------------------------------------------------====================
<img width="959" height="480" alt="Screenshot 2026-11-22 180934" src="https://github.com/user-attachments/assets/7c816ee5-24c8-4946-9ec5-e6fcabf83b88" />

<img width="959" height="504" alt="Screenshot 2026-11-22 122348" src="https://github.com/user-attachments/assets/ba8418d8-e688-4452-ab79-c0058c4e702b" />

<img width="693" height="325" alt="Screenshot 2026-11-22 181118" src="https://github.com/user-attachments/assets/7a81fdd9-a8a4-49eb-b4e3-d2f5ba44447c" />

<img width="959" height="495" alt="Screenshot 2026-11-22 182135" src="https://github.com/user-attachments/assets/111a0d08-1208-45fe-9135-3918288612bc" />

<img width="955" height="484" alt="Screenshot 2026-11-22 182328" src="https://github.com/user-attachments/assets/65285bd7-4456-4fd6-972a-a854fbe75abc" />

<img width="1536" height="1024" alt="BrightPath Scholarship System UI Showcase" src="https://github.com/user-attachments/assets/2069256a-0224-484a-9d08-f87c9cded411" />

****
