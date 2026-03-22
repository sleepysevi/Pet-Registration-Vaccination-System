```mermaid
graph TD
    ROOT[PetManagementSystem]
    
    ROOT --> README[README.md]
    ROOT --> MAIN[main.cs]
    ROOT --> PROJ[PetManagementSystem.csproj]
    
    ROOT --> ASSETS[assets]
    ASSETS --> IMAGES[images]
    IMAGES --> LOGO[logo.png]
    IMAGES --> SCREENS[screenshots]
    ASSETS --> ICONS[icons]
    ASSETS --> UI[ui]
    
    ROOT --> DOCS[docs]
    DOCS --> API[api.md]
    DOCS --> DEPLOY[deployment.md]
    
    ROOT --> MODELS[models]
    MODELS --> OWNER[Owner.cs]
    MODELS --> PET[Pet.cs]
    MODELS --> USER[User.cs]
    
    ROOT --> REPOS[repositories]
    REPOS --> OWNERREPO[OwnerRepository.cs]
    REPOS --> PETREPO[PetRepository.cs]
    
    ROOT --> CTRLS[controllers]
    CTRLS --> OWNERCTRL[OwnerController.cs]
    CTRLS --> AUTHCTRL[AuthController.cs]
    
    ROOT --> SRVS[services]
    SRVS --> AUTHSRV[AuthService.cs]
    SRVS --> QRSRV[QRService.cs]
    
    ROOT --> VIEWS[views]
    VIEWS --> LOGIN[LoginView.cs]
    VIEWS --> DASH[DashboardView.cs]
    VIEWS --> OWNERVIEW[OwnerView.cs]
    
    ROOT --> DB[database]
    DB --> DBCONN[DBConnection.cs]
    DB --> SCHEMA[schema.sql]
    DB --> DBREADME[README.md]
    
    %% VERTICAL LAYOUT + BLACK TEXT
    classDef root fill:#FFEBEE,stroke:#D32F2F,stroke-width:4px,color:#000000
    classDef folder fill:#E3F2FD,stroke:#1976D2,stroke-width:3px,color:#000000
    classDef file fill:#F5F5F5,stroke:#424242,stroke-width:2px,color:#000000
    classDef core fill:#E8F5E8,stroke:#388E3C,stroke-width:3px,color:#000000
    
    class ROOT root
    class ASSETS,DOCS,MODELS,REPOS,CTRLS,SRVS,VIEWS,DB,IMAGES,ICONS,UI folder
    class OWNER,PET,USER,OWNERREPO,PETREPO,OWNERCTRL,AUTHCTRL,AUTHSRV,QRSRV,LOGIN,DASH,OWNERVIEW,DBCONN,SCHEMA,DBREADME,LOGO,SCREENS,API,DEPLOY file
    class README,MAIN,PROJ core
