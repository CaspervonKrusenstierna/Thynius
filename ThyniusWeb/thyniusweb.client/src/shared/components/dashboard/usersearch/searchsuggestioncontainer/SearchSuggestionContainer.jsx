import React, { useEffect } from 'react'
import "./SearchSuggestionContainer.css"
import SearchSuggestion from './components/searchsuggestion/SearchSuggestion';

const SearchSuggestionContainer = (props) => {
  useEffect(() => {
    if(props.searchSuggestions){
      if(props.searchSuggestions.length == 1 && props.textInput == props.searchSuggestions[0].username){
        props.onChoose(props.searchSuggestions[0]);
      }  
    }
  }, [props.searchSuggestions])
  return (
    <div>
        <div className='SearchSuggestionContainer'>
            {props.searchSuggestions?.map(s => <SearchSuggestion key={s.id} Text={s.username} onClick={() => {props.onChoose(s)}} ></SearchSuggestion>)}
        </div>
    </div>
  )
}

export default SearchSuggestionContainer