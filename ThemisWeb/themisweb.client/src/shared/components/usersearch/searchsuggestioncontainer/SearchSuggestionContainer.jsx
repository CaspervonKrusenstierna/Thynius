import React from 'react'
import "./SearchSuggestionContainer.css"
import SearchSuggestion from './components/searchsuggestion/SearchSuggestion';

const SearchSuggestionContainer = (props) => {
  return (
    <div>
        <div className='SearchSuggestionContainer'>
            {props.searchSuggestions?.map(s => {<SearchSuggestion Text={s.username} onClick={() => {props.onChoose(s)}} ></SearchSuggestion>})}
        </div>
    </div>
  )
}

export default SearchSuggestionContainer