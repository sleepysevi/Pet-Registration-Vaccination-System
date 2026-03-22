```mermaid
graph TD
    ROOT[PetManagementSystem]:::root
    
    %% Main files
    README[README.md]:::doc
    MAIN[main.cs]:::core
    
    %% Assets
    ASSETS[/assets]:::assets
    ASSETS --> IMAGES[/images]:::assets
    IMAGES --> LOGO[logo.png]:::img
    IMAGES --> SCREENS[/screenshots/]:::assets
    ASSETS --> ICONS[/icons]:::assets
    ASSETS --> UI[/ui/]:::assets
    
    %% Docs
    DOCS[/docs]:::doc
    DOCS --> API[api.md]:::doc
    DOCS --> DEPLOY[deployment.md]:::doc
    
    %% Models
    MODELS[/models]:::model
    MODELS --> OWNER[Owner.cs]:::model
    MODELS --> PET[Pet.cs]:::model
    MODELS --> USER[User.cs]:::model
    
    %% Repositories
    REPOS[/repositories]:::db
    REPOS --> OWNERREPO[OwnerRepository.cs]:::db
    REPOS --> PETREPO[PetRepository.cs]:::db
    
    %% Controllers
    CTRLS[/controllers]:::ctrl
    CTRLS --> OWNERCTRL[OwnerController.cs]:::ctrl
    CTRLS --> AUTHCTRL[AuthController.cs]:::ctrl
    
    %% Services
    SRVS[/services]:::service
    SRVS --> AUTHSRV[AuthService.cs]:::service
    SRVS --> QRSRV[QRService.cs]:::service
    
    %% Views
    VIEWS[/views]:::view
    VIEWS --> LOGIN[LoginView.cs]:::view
    VIEWS --> DASH[DashboardView.cs]:::view
    VIEWS --> OWNERVIEW[OwnerView.cs]:::view
    
    %% Database
    DB[/database]:::db
    DB --> DBCONN[DBConnection.cs]:::db
    DB --> SCHEMA[schema.sql]:::db
    DB --> DBREADME[README.md]:::doc
    
    %% Project file
    PROJ[PetManagementSystem.csproj]:::core
    
    %% Root connections
    ROOT --> README
    ROOT --> MAIN
    ROOT --> ASSETS
    ROOT --> DOCS
    ROOT --> MODELS
    ROOT --> REPOS
    ROOT --> CTRLS
    ROOT --> SRVS
    ROOT --> VIEWS
    ROOT --> DB
    ROOT --> PROJ
    
    classDef root fill:#fff3e0,stroke:#f57c00,stroke-width:4px,color:#000
    classDef core fill:#e8f5e8,stroke:#388e3c,stroke-width:3px,color:#000
    classDef assets fill:#e3f2fd,stroke:#1976d2,stroke-width:3px,color:#000
    classDef doc fill:#f3e5f5,stroke:#7b1fa2,stroke-width:3px,color:#000
    classDef model fill:#fff3e0,stroke:#f57c00,stroke-width:3px,color:#000
    classDef db fill:#e0f2f1,stroke:#00796b,stroke-width:3px,color:#000
    classDef ctrl fill:#fce4ec,stroke:#c2185b,stroke-width:3px,color:#000
    classDef service fill:#f3e5f5,stroke:#7b1fa2,stroke-width:3px,color:#000
    classDef view fill:#e8f5e8,stroke:#388e3c,stroke-width:3px,color:#000
    classDef img fill:#e1f5fe,stroke:#0277bd,stroke-width:2px,color:#000
