DROP TABLE IF EXISTS Availabilities;

CREATE TABLE Availabilities (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Timeslots VARCHAR(500) NOT NULL,
    Services VARCHAR(500) NOT NULL 
);
