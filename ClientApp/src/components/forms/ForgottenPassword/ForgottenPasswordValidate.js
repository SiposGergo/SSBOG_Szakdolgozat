const validate = values => {
    const errors = {};
   
    if (!values.email) {
      errors.email = 'Az e-mail címedet kötelező megadni!';
    }
    if (!values.userName) {
        errors.userName = 'A felhasználóneved kötelező megadni!';
      }
    return errors;
  };
  
  export default validate;