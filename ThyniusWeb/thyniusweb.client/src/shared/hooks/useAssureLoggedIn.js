import { useContext, useEffect } from "react";
import { sessionInfoContext } from "../../App";
import { useNavigate } from "react-router-dom";

export default function AssureLoggedIn(){
    let sessionInfo = useContext(sessionInfoContext);
    const navigate = useNavigate();
    useEffect(() => {
        
        try{
            if(!sessionInfo.sessionInfo){
                navigate("/login");
            }
        }
        catch{
            navigate(0);
        }
    }, [])
}