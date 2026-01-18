# OfferManager Frontend

React + TypeScript frontend for the OfferManager application.

## Getting Started

### Prerequisites
- Node.js 18+
- npm or yarn

### Installation

```bash
cd frontend
npm install
```

### Development

```bash
npm run dev
```

The app will be available at `http://localhost:5173`

### Build for Production

```bash
npm run build
```

## Project Structure

```
src/
├── components/     - React components (OfferList, CustomerList, etc.)
├── pages/          - Page components for routing
├── services/       - API service layer
├── App.tsx         - Main app component with routing
├── main.tsx        - Entry point
└── index.css       - Global styles
```

## Environment Variables

- `.env.development` - Development environment variables
- `.env.production` - Production environment variables

## API Integration

The frontend uses Axios to communicate with the backend API. API services are located in `src/services/`:

- `api.ts` - Axios client configuration
- `offerService.ts` - Offer CRUD operations
- `customerService.ts` - Customer CRUD operations
- `userService.ts` - User CRUD operations

## Features

- ✅ Browse Offers
- ✅ Browse Customers
- ✅ Responsive navigation
- 🚧 Create/Edit/Delete operations (UI ready, handlers needed)
- 🚧 User management
- 🚧 Form validation
- 🚧 Error handling improvements
