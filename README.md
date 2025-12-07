# Search Aggregator Function - Full Stack Project

A web application that aggregates search results from multiple search engines (Google Custom Search API and Wikipedia) and displays combined hit counts.

## 📋 Table of Contents
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Access Points](#access-points)

---

## 🛠 Technologies Used

### Backend
- **C#** / ASP.NET Core 8.0+ - RESTful API

### Frontend
- **React 19**
- **TypeScript**
- **CSS**

---

## 📁 Project Structure

```
SearchAggregator/
│
├── Backend/                          # C# ASP.NET Core API
│   ├── Search.API/
│   │   ├── Controllers/
│   │   │   └── SearchController.cs   # Main API endpoint
│   │   ├── Models/
│   │   │   ├── SearchRequest.cs
│   │   │   ├── SearchResponse.cs
│   │   │   └── ProviderResult.cs
│   │   ├── Services/
│   │   │   ├── Interfaces/
│   │   │   │   └── ISearchEngine.cs
│   │   │   ├── GoogleSearchService.cs
│   │   │   └── WikipediaSearchService.cs
        |--- Providers/
│   │   │   ├── Interfaces/
│   │   │   │   └── ISearchEngine.cs
│   │   │   ├── GoogleEngineProvider.cs
            |--- GoogleEngineProvider.cs
│   │   │   └── WikipediaEngineProvidere.cs
│   │   ├── Program.cs                # App configuration
│   │   └── appsettings.json          # Configuration
│   │
│   └── Search.API.sln
│
└── Frontend/                         # React TypeScript App
    ├── src/
    │   ├── components/               # React components
    │   │   ├── SearchBox.tsx
    │   │   ├── ProviderCard.tsx
    │   │   ├── SearchResults.tsx
    │   │   ├── ErrorMessage.tsx
    │   │   └── Loader.tsx
    │   ├── hooks/
    │   │   └── useCache.ts           # Custom cache hook
    │   ├── service/
    │   │   └── searchService.ts      # API client
    │   ├── types/
    │   │   └── searchtypes.ts        # TypeScript interfaces
    │   ├── utils/
    │   │   └── helpers.ts            # Utility functions
    │   ├── App.tsx                   # Main component
    │   ├── App.css                   # Styles
    │   └── main.tsx                  # Entry point
    │
    ├── package.json
    └── vite.config.ts
```

---

## ✅ Prerequisites

Before running the project, ensure you have:

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- [Node.js 18+ and npm](https://nodejs.org/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- **Google Custom Search API Key & Google Search Engine ID (cx)** ([Get it here](https://developers.google.com/custom-search/v1/overview)) (I didint actually remove it from appsettings so its easier to review 😉😁)
- **Wikipedia API sand box** ([Wiki link here](https://en.wikipedia.org/wiki/Special:ApiSandbox#action=query&list=search&srsearch=Nelson%20Mandela&utf8=&format=json
)) (No API keys Needs 🕺)
---

## 🚀 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Youssefhikal93/SearchEngine.git
cd SearchAggregator
```

### 2. Backend Setup

#### A. Navigate to Backend Directory
```bash
cd Search.API
```
#### C. Restore Dependencies
```bash
dotnet restore
```
#### D. Build the Project
```bash
dotnet build
```
#### E. Run the API 
```bash
dotnet run
```
### 3. Frontend Setup

#### A. Navigate to Frontend Directory (open new terminal at the root of the project)
```bash
cd Search.web
```

#### B. Install Dependencies
```bash
npm install
```
#### B. Run the web app
```bash
npm run dev
```
---

## 🌐 Access Points

Once both are running:

| Service | URL | Description |
|---------|-----|-------------|
| **Frontend** | http://localhost:5173 | Main user interface |
| **Backend API** | http://localhost:5138 | REST API |
| **Swagger UI** | http://localhost:5138/swagger | Interactive API documentation |

> **Note**: Port numbers may vary. Check your console output for actual ports. Otherwise the application might not start since the Frontend consuming hardcoded URL 🤷‍♀️
> as you might need to change the API_URL in case the port number is differnet in the follwoing file **Search.web/service/searchService.ts**

---

**Happy Review! 💻👩‍💻**
