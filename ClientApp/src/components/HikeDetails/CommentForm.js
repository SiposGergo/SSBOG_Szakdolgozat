import React from 'react'
import { Field, reduxForm } from 'redux-form'

const CommentForm = props => {
  const { handleSubmit, pristine, reset, submitting } = props
  return (
    <form onSubmit={handleSubmit}>
    <p>Írj kommentet!</p>
    <div>
    <div>
      <Field
        name="message"
        component="textarea"
        placeholder="Üzenet..."
        rows = "8"
        cols = "50"
      />
    </div>
    </div>
      <div>
        <button type="submit" disabled={pristine || submitting}>
          Elküld
        </button>
      </div>
    </form>
  )
}

export default reduxForm({
  form: 'CommentForm'
})(CommentForm)