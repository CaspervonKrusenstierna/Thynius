import useFetch from "../../../../shared/hooks/useFetch"

function useInitializeAccount() {

    return useFetch("/account/initializeaccount", "POST")
}
export default useInitializeAccount;