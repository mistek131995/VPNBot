pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
                sh 'mkdir /home/source'
            }
        }
        stage('Create container') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Delete source') {
            steps {
                sh 'rm -R /home/source'
            }
        }
    }
}