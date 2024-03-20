import React from 'react'
import "./SearchSuggestion.css"

const SearchSuggestion = (props) => {
  return (
    <button className="SearchSuggestion" onClick={props.onClick}>{props.Text}</button>
  )
}

export default SearchSuggestion