import validator from "validator";

const validate = values => {
    const errors = {};
   
    if (!values.name) {
      errors.name = 'A túra nevét kötelező megadni!';
    }
    if (!values.description) {
        errors.description = 'A Leírást kötelező megadni!';
      }
    if  (values.website && !validator.isURL(values.website)) {
        errors.website = "Érvényes weboldal címet adj meg";
    }
    if (!values.date) {
        errors.date = "Kötelező megadni a túra dátumát!";
    }
    return errors;
  };
  
  export default validate;
  