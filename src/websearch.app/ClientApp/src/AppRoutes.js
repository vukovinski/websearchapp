import { Results } from "./components/Results";
import { WebSearchPage } from "./components/WebSearch";

const AppRoutes = [
  {
    index: true,
    element: <WebSearchPage />
  },
  {
    path: '/results',
    element: <Results />
  }
];

export default AppRoutes;
