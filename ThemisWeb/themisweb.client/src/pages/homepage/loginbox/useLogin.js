import useFetch from "../../../../shared/hooks/useFetch"

 function useLogin(Email, Password, RememberMe) {

     return useFetch(`/login?useCookies=${RememberMe}`, "POST", { "email": Email, "password": Password })
}
export default useLogin