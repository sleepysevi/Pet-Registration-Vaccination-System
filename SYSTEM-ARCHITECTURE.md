```mermaid
%% PET REGISTRATION SYSTEM AND VACCINE MONITORING SYSTEM
graph TB
    %% 1. BIGGER SYSTEM ARCHITECTURE
    subgraph arch["SYSTEM ARCHITECTURE"]
        U[" USER<br/>Admin/Staff"] --> PL[" WINFORMS UI<br/>Forms Grids"]
        PL --> LL[" C# LOGIC<br/>Validation Rules"]
        LL --> DL[" MYSQL DB<br/>All Tables"]
    end

    %% 2. DATABASE ERD
    subgraph db["DATABASE ERD"]
        USERS[" USERS<br/>userID PK<br/>username<br/>role"]
        OWNERS[" OWNERS<br/>ownerID PK<br/>name<br/>phone"]
        PETS[" PETS<br/>petID PK<br/>ownerID FK"]
        VACC[" VACC<br/>petID FK<br/>vaccine<br/>dates"]
        HIST[" HEALTH HIST<br/>petID FK<br/>notes<br/>date"]
        LOST[" LOST PETS<br/>petID FK<br/>location"]
        
        OWNERS -->|"1:MANY"| PETS
        PETS -->|"1:MANY"| VACC
        PETS -->|"1:MANY"| HIST
        PETS -->|"1:1 OPT"| LOST
    end

    %% 3. BIGGER WORKFLOW
    subgraph flow["MAIN WORKFLOW"]
        A[" LOGIN"] --> B[" DASHBOARD"]
        B --> C[" OWNER REG"]
        C --> D[" PET REG"]
        D --> E[" VACCINATION"]
        E --> F[" QR CODE"]
        F --> G[" SEARCH"]
        G --> H[" LOST PET"]
    end

    %% 4. BIGGER DATA FLOW
    subgraph data["DATA FLOW"]
        INPUT[" FORM INPUT"] --> VALID[" C# CHECKS"]
        VALID --> DB[" DB SAVE"]
        DB --> QUERY[" SQL QUERY"]
        QUERY --> OUT[" DISPLAY"]
    end

    %% 5. BIGGER ROLES
    subgraph roles["ROLE ACCESS"]
        LOGIN2["LOGIN OK"] --> CHK["CHECK ROLE"]
        CHK --> ADM[" ADMIN<br/>FULL ACCESS"]
        CHK --> STF[" STAFF<br/>REGISTER AND CRUD ONLY"]
    end

    %% 6. BIGGER QR
    subgraph qr["QR SYSTEM"]
        REG["PET CREATED"] --> GET["GET petID+PHONE"]
        GET --> MAKE["MAKE QR CODE"]
        MAKE --> SHOW["SHOW PRINT"]
        UPD["UPDATE PHONE"] --> REGEN["REGEN QR"]
    end

    %% CONNECTIONS
    U -.-> A
    PL -.-> B
    LL -.-> C
    DL -.-> PETS
    F -.-> MAKE
    G -.-> QUERY

    %% ENHANCED COLORS + BIGGER FONTS
    classDef ui fill:#E3F2FD,stroke:#1976D2,stroke-width:4px,color:#000000,font-size:14px
    classDef db fill:#F3E5F5,stroke:#7B1FA2,stroke-width:4px,color:#000000,font-size:14px
    classDef flow fill:#E8F5E8,stroke:#388E3C,stroke-width:4px,color:#000000,font-size:14px
    classDef data fill:#FFF3E0,stroke:#F57C00,stroke-width:4px,color:#000000,font-size:14px
    classDef roles fill:#FCE4EC,stroke:#C2185B,stroke-width:4px,color:#000000,font-size:14px
    classDef qr fill:#E0F2F1,stroke:#00796B,stroke-width:4px,color:#000000,font-size:14px
    
    class U,PL,LL,DL ui
    class USERS,OWNERS,PETS,VACC,HIST,LOST db
    class A,B,C,D,E,F,G,H flow
    class INPUT,VALID,DB,QUERY,OUT data
    class LOGIN2,CHK,ADM,STF roles
    class REG,GET,MAKE,SHOW,UPD,REGEN qr
