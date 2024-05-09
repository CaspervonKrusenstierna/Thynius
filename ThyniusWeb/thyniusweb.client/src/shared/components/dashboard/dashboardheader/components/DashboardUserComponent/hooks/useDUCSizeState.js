import { useEffect, useState } from "react";

export default function useDUCSizeState(){
    const [sizeState, setSizeState] = useState(true);
    function onResize(){
        let width = window.innerWidth;
          if(width < 650){
            setSizeState(false);
          }
          else{
            setSizeState(true);
          }
        }
      useEffect(() => {
          onResize();
          window.addEventListener("resize", onResize);
      }, []);
    return sizeState
}