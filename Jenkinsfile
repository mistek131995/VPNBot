pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
                mkdir /home/source
            }
        }
        stage('Create container') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Delete source') {
            steps {
                rm -R /home/source
            }
        }
    }
}