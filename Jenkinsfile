pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
				script {
					res = sh(script: "test -d /home/source && echo '1' || echo '0' ", returnStdout: true).trim()
					if(res=='1'){
						sh 'rm -R /home/source'
					}				
				}
			
                sh 'mkdir /home/source'
				sh 'git clone https://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@github.com/mistek131995/VPNBot.git /home/source'
            }
        }
        stage('Create image') {
            steps {
                script{
                    sh 'docker build -t vpn-api-${currentBuild.number} /home/source'
                }
            }
        }
		stage('Stop container') {
            steps {
                echo 'Testing..'
            }
        }
		stage('Start container') {
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