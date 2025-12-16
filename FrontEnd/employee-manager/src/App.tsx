import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import BaseLayout from './components/base-layout';
import ListEmployee from './pages/ListEmployee';


function App() {

  return (
    <>
      <BrowserRouter>
        <BaseLayout>
          <Routes>
            <Route path="/" element={<ListEmployee />} />
          </Routes>
        </BaseLayout>
      </BrowserRouter>
    </>
  )
}

export default App
