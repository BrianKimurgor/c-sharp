# **Portfolio Backend and Frontend Documentation**

## **Overview**
This project aims to build a **dynamic portfolio** to showcase skills, projects, badges, blog posts, and contact information using modern technologies. The frontend will be built using **Next.js**, **Tailwind CSS**, **Ripple UI**, **Framer Motion**, and **TypeScript**. The backend will be implemented with **.NET Core**, **PostgreSQL** for the database, and will follow a **microservice architecture**.

## **Features**
- **Home Page**: Name, profession, profile image, and social links. Dark mode toggle and animations on page load.
- **About Page**: Personal bio, skills, and career timeline.
- **Projects Page**: List of projects with tech stack, description, and links to GitHub/live site.
- **Badges Page**: List of certifications and achievements.
- **Blog Page**: Display blog posts with individual pages for each post.
- **Contact Page**: Contact form to send messages, along with social media links.
- **Work Page**: Current work experience with role, responsibilities, and tech stack.
- **Admin Panel**: Authentication for managing blog posts, projects, badges, and work experience.
- **Dark Mode Toggle**: User preferences saved across sessions.
- **Animations**: Page transitions, hover effects, scroll reveals using **Framer Motion**.
- **Search/Filtering**: Ability to filter projects and badges by tags or tech stack.
- **Downloadable CV/Resume**: Users can download a PDF version of the resume.

---

## **Tech Stack**

### **Frontend**
- **Next.js**: React-based framework for server-side rendering (SSR) and static site generation (SSG).
- **Tailwind CSS**: Utility-first CSS framework for responsive design.
- **Ripple UI**: A UI component library built on top of Tailwind for faster development.
- **Framer Motion**: For animations, page transitions, and scroll reveals.
- **TypeScript**: Adding type safety for the frontend code.
- **React**: For building user interfaces with reusable components.
- **React Context API**: For managing global states like user authentication and theme.
- **Vercel**: Deployment for the Next.js frontend.

### **Backend**
- **.NET Core**: Framework for building REST APIs and microservices.
- **PostgreSQL**: Relational database for storing data like projects, badges, blog posts, and more.
- **Microservice Architecture**: Each service will handle specific domain logic (e.g., `ProjectService`, `AuthService`, etc.).
- **JWT Authentication**: For user login and session management in the Admin Panel.
- **Swagger**: API documentation for ease of interaction.
- **Docker**: For containerizing services and managing dependencies.
- **Azure / Railway / Render**: Cloud deployment platforms for the backend.

---

## **Project Structure**

### **Frontend Project Structure**

```
/portfolio-frontend
├── /components
│   ├── /common          # Reusable UI components (Button, Navbar)
│   ├── /layout          # Layout components (Header, Footer)
│   ├── /sections        # Section-based components (HeroSection, ProjectCard)
│   ├── /forms           # Form components (ContactForm, BlogPostForm)
│   └── /animations      # Framer Motion animations (fadeIn, slideIn)
├── /pages
│   ├── /api             # API routes (SSR, SSG)
│   ├── /about           # About page (About.js)
│   ├── /projects        # Projects page (Projects.js)
│   ├── /badges          # Badges page (Badges.js)
│   ├── /contact         # Contact page (Contact.js)
│   ├── /blog            # Blog pages (Blog.js, Post.js)
│   ├── /work            # Work page (Work.js)
│   └── /index.js        # Home page
├── /public              # Static assets (images, icons)
│   ├── /images
│   └── /icons
├── /styles              # Global and component-specific styles (Tailwind CSS)
│   ├── /globals.css     # Global styles, Tailwind config
│   └── /tailwind.config.js
├── /utils               # Utility functions (API helpers, date formatting)
├── /hooks               # Custom hooks (useAuth, useDarkMode)
├── /contexts            # Global state management (theme, auth)
├── /services            # API services (ProjectService, AuthService)
├── /lib                 # External libraries or helpers (Framer Motion setup)
├── .env.local           # Environment variables (API keys, backend URLs)
├── next.config.js       # Next.js config
└── package.json         # Dependencies & scripts
```

### **Backend Project Structure**

```
/portfolio-backend
├── /services
│   ├── /AuthService
│   ├── /BadgeService
│   ├── /BlogService
│   ├── /ContactService
│   ├── /ProfileService
│   ├── /ProjectService
│   ├── /WorkService
│   └── /SocialService (optional)
├── /shared
│   ├── /DTOs
│   ├── /Common (base classes, error handlers)
│   ├── /Middleware (auth validation, logging)
│   ├── /Utils
│   └── /Constants
├── /gateway (optional, for API Gateway)
├── docker-compose.yml
├── README.md
└── .env / config files
```

---

## **Service Breakdown**

### 🧩 **Inside Each Service (e.g., ProjectService)**

Each service in the backend follows a modular, isolated structure. Here’s a breakdown of the **ProjectService**:

```
/ProjectService
├── /Controllers       # Controllers that handle incoming HTTP requests
├── /Services          # Core logic and business rules for the service
├── /Repositories      # Data access layer (communicates with PostgreSQL)
├── /Models            # Entity models representing data
├── /DTOs              # Data Transfer Objects for clean API interaction
├── /Migrations        # Database schema migrations (using Entity Framework)
├── /Configs           # Service-specific configuration (e.g., database connection)
├── Program.cs          # Service entry point and setup
├── appsettings.json    # Configuration settings (API keys, database settings)
└── ProjectService.csproj  # Project file
```

Each service is **self-contained** and can:
- Have its own **database** (or schema if using the same DB).
- Be **containerized independently** for isolated deployment.
- Use **own authentication policies** if needed (e.g., for handling user access to specific service resources).

---

## **Naming Conventions**
- **CamelCase** will be used for variables, functions, and components (e.g., `userProfile`, `getUserData`, `ProjectCard`).
- **PascalCase** for component names and service class names (e.g., `ProjectService`, `AuthService`).
- **snake_case** for configuration files and environment variables (e.g., `DB_URL`, `JWT_SECRET`).

---

## **Unit Testing**
- **Frontend**: Unit tests will be written using **Jest** and **React Testing Library** for component-level testing.
- **Backend**: **xUnit** will be used for unit testing in .NET Core.

---

## **Next Steps**
1. **Frontend**: Set up Next.js with Tailwind, Ripple UI, and Framer Motion for UI components and animations.
2. **Backend**: Start building microservices (begin with `ProjectService`).
3. **Authentication**: Implement JWT for secure Admin Panel access.
4. **Testing**: Write unit tests for components, API endpoints, and services.

---