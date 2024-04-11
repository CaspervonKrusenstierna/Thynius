import useFetch from "../../../shared/hooks/useFetch"

 function useForgotPassword(Email, Password, RememberMe) {

     return useFetch(`/ForgotPassword`, "POST", { "email": Email})
}
export default useForgotPassword