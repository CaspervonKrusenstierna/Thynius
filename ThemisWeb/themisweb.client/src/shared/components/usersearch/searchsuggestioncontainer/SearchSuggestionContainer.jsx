import React from 'react'
import "./SearchSuggestionContainer.css"
import useSearchSuggestions from '../useSearchSuggestions'
import SearchSuggestion from './components/searchsuggestion/SearchSuggestion';

const SearchSuggestionContainer = (props) => {
  const searchSuggestions = useSearchSuggestions(props.search);

  return (
    <div>
        <div className='SearchSuggestionContainer'>
            {searchSuggestions?.map(s => {if(!props.disable){return <SearchSuggestion Text={s.username} onClick={() => {props.onChoose(s)}} ></SearchSuggestion>}})}
        </div>
    </div>
  )
}

export default SearchSuggestionContainer