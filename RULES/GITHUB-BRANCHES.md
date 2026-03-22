```mermaid
graph TD
    ROOT[PetManagementSystem]
    
    README[README.md]
    MAIN[main.cs]
    PROJ[PetManagementSystem.csproj]
    
    ASSETS[assets]
    ASSETS --> IMAGES[images]
    IMAGES --> LOGO[logo.png]
    IMAGES --> SCREENS[screenshots]
    ASSETS --> ICONS[icons]
    ASSETS --> UI[ui]
    
    DOCS[docs]
    DOCS --> API[api.md]
    DOCS --> DEPLOY[deployment.md]
    
    MODELS[models]
    MODELS --> OWNER[Owner.cs]
    MODELS --> PET[Pet.cs]
    MODELS --> USER[User.cs]
    
    REPOS[repositories]
    REPOS --> OWNERREPO[OwnerRepository.cs]
    REPOS --> PETREPO[PetRepository.cs]
    
    CTRLS[controllers]
    CTRLS --> OWNERCTRL[OwnerController.cs]
    CTRLS --> AUTHCTRL[AuthController.cs]
    
    SRVS[services]
    SRVS --> AUTHSRV[AuthService.cs]
    SRVS --> QRSRV[QRService.cs]
    
    VIEWS[views]
    VIEWS --> LOGIN[LoginView.cs]
    VIEWS --> DASH[DashboardView.cs]
    VIEWS --> OWNERVIEW[OwnerView.cs]
    
    DB[database]
    DB --> DBCONN[DBConnection.cs]
    DB --> SCHEMA[schema.sql]
    DB --> DBREADME[README.md]
    
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
    
    classDef folder fill:#e3f2fd,stroke:#1976d2,stroke-width:2px
    classDef file fill:#f3e5f5,stroke:#7b1fa2,stroke-width:1px
    classDef rootfile fill:#e8f5e8,stroke:#388e3c,stroke-width:3px
    
    class ASSETS,DOCS,MODELS,REPOS,CTRLS,SRVS,VIEWS,DB,IMAGES,ICONS,UI folder
    class OWNER,PET,USER,OWNERREPO,PETREPO,OWNERCTRL,AUTHCTRL,AUTHSRV,QRSRV,LOGIN,DASH,OWNERVIEW,DBCONN,SCHEMA,DBREADME,LOGO,SCREENS,API,DEPLOY file
    class ROOT,README,MAIN,PROJ rootfile
