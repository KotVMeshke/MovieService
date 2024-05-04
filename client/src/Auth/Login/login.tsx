import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './login.css';



const LoginForm = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const handleFormSubmission = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setLoading(true);
  
    try {
      const response = await fetch(`http://localhost:5025/api/v1.0/user/authorization?email=${email}&password=${password}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        },
      });
      console.log(response);
      if (response.status == 302) {
        const data = await response.json();
        console.log('Login successful:', data);
        localStorage.setItem('authorized', 'true');
        localStorage.setItem('user_id', "");
        navigate("/hub");
      } else {
        console.error('Login failed:', response.statusText);
      }
    } catch (error) {
      console.error('Error during login:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="login-form-container" onSubmit={handleFormSubmission}>
      <div className="login-form">
        <h1 className="login-form__title">Вход</h1>
        <label className="login-form__label">
          Email:
          <input
            className="login-form__input"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
          />
        </label>
        <br />
        <label className="login-form__label">
          Пароль:
          <input
            className="login-form__input"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </label>
        <br />
        <div className="login-form__checkbox">
          <input type="checkbox" id="rememberMe" />
          <label htmlFor="rememberMe">Запомнить меня</label>
        </div>
        <br />
        <button className="login-form__button" type="submit">
          {loading? 'Loading...' : 'Войти'}
        </button>
        <div className="has-not-account">
          <span>Ещё не зарегистрированы?</span>
          <a href="/registration">Регистрация</a>
        </div>
      </div>
    </form>
  );
};

interface UserData {
  id: number;
  name: string;
}

export default LoginForm;