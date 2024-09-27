## Electronic Voting System

![Project](https://github.com/user-attachments/assets/5f72dd7d-3d22-4fe3-9dbf-1556a62dc3de)


## Description
The Electronic Voting System is a C# application that allows users to manage and participate in elections. The system supports admin functionalities like managing voters, elections, candidates, and vote results, as well as voter registration and voting.

## Prerequisites

- Windows7/Windows8/Windows10/Windows11
- Visual Studio 2017 upto 22
- .NET Framework

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/roshninaznin/Electronic-Voting-System
    ```
3. click open .sln file
4. Go to Server Explorer
5. Set up the SQL Server and ensure the `vote.mdf` database file is correctly placed in the project.
6. Right Click vote.mdf and go to Properties
7. Copy connection String
8. Open DB.cs on VotingSystemRepository
9. Edit ,and Paste the connection String And Save
10.For demo data for Admin ID : 1, Username: Admin and Password : manager123
14. Run the project.

## Modules
 - Admin
 - Voter

## Usage
- Admins can:
  - Log in, manage voters, elections, and candidates.
  - View and monitor vote results.
  
![Admin](https://github.com/user-attachments/assets/000f9bf0-f5bc-46c5-a5ad-f34f8ff56ed3)


- Voters can:
  - Register, log in, select an election, vote and view results.
    
![Voter](https://github.com/user-attachments/assets/c57ac82d-4ed7-4e7c-8d1e-4ac90d584908)
    

## Features

- Can add multiple election, types: Club Leader Election, Player Election, Faculty Election, Actor Election, etc.
- Secure voting system with tamper detection.
- Dynamic management of voters and elections.
  
## Built With
 - C# - The language
 - Ms sql - Used Database

## Contributing

Feel free to submit pull requests or report issues. Follow the contribution guidelines mentioned in the repository.

## Contact

For more information, please contact:  

**Naznin Akter Roshmny** 
- Email: roshninaznin202@gmail.com
- Phone: 01956494298
