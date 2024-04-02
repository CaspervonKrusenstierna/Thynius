import React, { useMemo } from 'react'
import {
    ShadowPagination,
    PaginationContent,
    PaginationLink,
    PaginationNext,
    PaginationPrevious,
  } from "@/components/ui/pagination"

const Pagination = (props) => {
      const AvailablePages = useMemo(() => {
        let currPage = props.currentPage;

        if(props.highestPage == 0){
          return [1];
        }
        if(currPage > props.highestPage && props.highestPage != null){
            props.setCurrentPage(currPage-1);
        }
        if(currPage == 1){
          switch(currPage){
            case (props.highestPage): return [1];
            case (props.highestPage-1): return [1,2];
            default: return [1,2,3];
          }
        }
        else{
          if(currPage == props.highestPage){
            if(currPage==2){
              return [1,2]
            } 
            else{
              return [currPage-2, currPage-1, currPage]
            }
          }
          return [currPage-1, currPage, currPage+1]
        }
      }, [props.currentPage, props.highestPage])

  return (
        <ShadowPagination>
            <PaginationContent>
            {(props.currentPage != 1) ? <PaginationPrevious href="#" text="Förra" onClick={() => {props.setCurrentPage(props.currentPage-1)}}/> : <div className='w-[82.45px]'></div>}
                {AvailablePages?.map((i, index) => {return <PaginationLink isActive={props.currentPage==AvailablePages[index]} onClick={() => {props.setCurrentPage(AvailablePages[index])}}>{AvailablePages[index]}</PaginationLink>})}
                {(props.currentPage != props.highestPage && props.highestPage != 0) ? <PaginationNext href="#" text="Nästa" onClick={() => {props.setCurrentPage(props.currentPage+1)}}/> : <div className='w-[82.45px]'></div>}
            </PaginationContent>
        </ShadowPagination>
  )
}

export default Pagination