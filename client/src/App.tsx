

import React from 'react';
import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Link, Outlet, useNavigate } from 'react-router-dom';
import LoginForm from './Auth/Login/login';
import RegistrationForm from './Auth/Registration/registration';
import MainPage from './Main/main';
import NotFound from './Components/404page';
import Library from './Library/library';
import Film from './Film/film';
import CrewMember from './CrewMember/crewMemeber';

const App = () => {
  return (
   
    <Router>
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/registration" element={<RegistrationForm />} />
        <Route path="/" element={<MainPage />}/>
        <Route path="/library" element={<Library />}/>
        <Route path="/film/:id" element={<Film />}/>
        <Route path="/crew/:crewId" element={<CrewMember />}/>
        <Route path='*' element={<NotFound />} />
      </Routes>
    </Router>
  );
};


export default App;
