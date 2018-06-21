import React from 'react'
import { Field, reduxForm } from 'redux-form'

const CommentForm = props => {
  const { handleSubmit, pristine, reset, submitting } = props
  return (
    <form onSubmit={handleSubmit}>
    <h3>Írj kommentet!</h3>
    <div>
    <div>
      <Field
        name="message"
        component="textarea"
        placeholder="Üzenet..."
        rows = "4"
        cols = "50"
      />
    </div>
    </div>
      <div>
        <button className="btn btn-green" type="submit" disabled={pristine || submitting}>
          Elküld
        </button>
      </div>
    </form>
  )
}

export default reduxForm({
  form: 'CommentForm'
})(CommentForm)