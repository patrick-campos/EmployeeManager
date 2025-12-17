import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import BaseLayout from './components/base-layout';
import ListEmployee from './pages/ListEmployee';
import { EmployeeForm } from './pages/EmployeeForm';


function App() {

  return (
    <>
      <BrowserRouter>
        <BaseLayout>
          <Routes>
            <Route path="/" element={<ListEmployee />} />
            <Route path="/employee/:id?" element={<EmployeeForm />} />
          </Routes>
        </BaseLayout>
      </BrowserRouter>
    </>
  )
}

export default App
