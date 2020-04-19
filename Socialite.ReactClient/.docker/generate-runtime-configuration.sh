#!/bin/bash
set -euf -o pipefail

config_file=$1

function reset_file {
    rm -rf $config_file
    
    touch $config_file
}

function write_config {
    unset IFS

    args=()

    echo "window.runtime = {" >> $config_file

    for var in $(compgen -e); do
        check_key $var ${!var}
    done

    echo "};" >> $config_file
}

# function fix_json {
#     res=$(cat $config_file | awk 'NR>2{print a;} {a=b; b=$0} END{sub(/,$/, "", a); print a;print b;}')

#     echo $res > $config_file
# }

function check_key {
    PREFIX='^REACT_APP_RUNTIME_([A-Z]+)$'

    if [[ $1 =~ ${PREFIX} ]]; then
        value=$2

        name=$(echo "${BASH_REMATCH[1]}" | awk '{print tolower($0)}')

        echo "\"${name}\": \"${value}\"," >> $config_file
    fi
}

reset_file

write_config
