const validate = values => {
    const errors = {};
   
    if (!values.currentPassword) {
      errors.currentPassword = 'A jelenlegi jelszavadat kötelező megadni!';
    }
    if (!values.newPassword) {
        errors.newPassword = 'Az új jelszavad kötelező megadni!';
      }
    return errors;
  };
  
  export default validate;