This project is a simple library system built with C# and SQL Server. 
The system manages the registration of books, members, and loans, as well as the return of borrowed books. 
Users can view active loans, search for books, and get an overview of a member’s loan history. 

Reflection on optimization and data integrity:
To optimize performance, I implemented indexes on primary keys, foreign keys, and columns frequently used in searches and filtering. 
This reduces the load on the database and makes common operations—such as displaying active loans or searching for books—much faster.
Data integrity is ensured through the use of transactions, especially in the loan and return logic. 
Transactions guarantee that multiple dependent operations either succeed together or fail together, preventing the database from ending up in an inconsistent state. 
I also added logic that prevents a book from being borrowed more than once at the same time.

<img width="1525" height="515" alt="Skärmbild 2026-01-04 211738" src="https://github.com/user-attachments/assets/74d7344c-3d56-4866-a23f-aa10955d8d2e" />
<img width="1919" height="623" alt="Skärmbild 2026-01-04 212501" src="https://github.com/user-attachments/assets/1358b2bd-4773-47f2-ab20-4a4f09a5a688" />
