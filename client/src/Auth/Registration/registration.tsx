import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './registration.css';

const RegistrationForm = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const handleFormSubmission = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setLoading(true);
    console.log(name + ' ' + email + ' ' + password);

    try {
      const response = await fetch(`http://localhost:5025/api/v1.0/user/registration?name=${name}&email=${email}&password=${password}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
      });
      console.log(response);
      if (response.status == 201) {
        const data = await response.json();
        console.log('Registration successful:', data);
        localStorage.setItem('authorized', 'true');
        navigate("/");

      } else {
        console.error('Registration failed:', response.statusText);
        // Show an error message to the user
      }
    } catch (error) {
      console.error('Error during registration:', error);
      // Show an error message to the user
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="registration-form-container" onSubmit={handleFormSubmission}>
      <div className="registration-form">
        <h1 className="registration-form__title">Регистрация</h1>
        <label className="registration-form__label">
          Имя:
          <input
            className="registration-form__input"
            type="text"
            value={name}
            onChange={(event) => setName(event.target.value)}
          />
        </label>
        <br />
        <label className="registration-form__label">
          Email:
          <input
            className="registration-form__input"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
          />
        </label>
        <br />
        <label className="registration-form__label">
          Пароль:
          <input
            className="registration-form__input"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </label>
        <br />
        <div className="registration-form__checkbox">
          <input type="checkbox" id="termsAndConditions" />
          <label htmlFor="termsAndConditions">Я согласен с условиями</label>
        </div>
        <br />
        <button className="registration-form__button" type="submit">
          {loading ? 'Loading...' : 'Зарегистрироваться'}
        </button>
        <div className="already-registered">
          <span>Уже зарегистрированы?</span>
          <a href="/login">Войти</a>
        </div>
      </div>
    </form>
  );
};

export default RegistrationForm;